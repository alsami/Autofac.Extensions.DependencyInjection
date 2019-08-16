// This software is part of the Autofac IoC container
// Copyright © 2019 Autofac Contributors
// https://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Autofac.Extensions.DependencyInjection
{
    /// <summary>
    /// TODO.
    /// </summary>
    public class AutofacChildScopeServiceProviderFactory : IServiceProviderFactory<AutofacChildScopeConfigurationAdapter>
    {
        private readonly Action<ContainerBuilder> _containerConfigurationAction;
        private readonly ILifetimeScope _rootLifetimeScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacChildScopeServiceProviderFactory"/> class.
        /// </summary>
        /// <param name="getRootLifetimeScopeFunc">TODO 1.</param>
        /// <param name="containerConfigurationAction">TODO 2.</param>
        public AutofacChildScopeServiceProviderFactory(Func<ILifetimeScope> getRootLifetimeScopeFunc, Action<ContainerBuilder> containerConfigurationAction = null)
        {
            if (getRootLifetimeScopeFunc == null) throw new ArgumentNullException(nameof(getRootLifetimeScopeFunc));

            _rootLifetimeScope = getRootLifetimeScopeFunc();
            _containerConfigurationAction = containerConfigurationAction ?? (builder => { });
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="services">TODO 1.</param>
        /// <returns>TODO 2.</returns>
        public AutofacChildScopeConfigurationAdapter CreateBuilder(IServiceCollection services)
        {
            var actions = new AutofacChildScopeConfigurationAdapter();

            actions.Add(builder => builder.Populate(services));
            actions.Add(builder => _containerConfigurationAction(builder));

            return actions;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="autofacChildScopeConfigurationAdapter">TODO 1.</param>
        /// <returns>TODO 2.</returns>
        public IServiceProvider CreateServiceProvider(AutofacChildScopeConfigurationAdapter autofacChildScopeConfigurationAdapter)
        {
            if (autofacChildScopeConfigurationAdapter == null) throw new ArgumentNullException(nameof(autofacChildScopeConfigurationAdapter));

            var scope = _rootLifetimeScope.BeginLifetimeScope(scopeBuilder =>
            {
                foreach (var action in autofacChildScopeConfigurationAdapter.ChildScopeConfigurationActions)
                {
                    action(scopeBuilder);
                }
            });

            return new AutofacServiceProvider(scope);
        }
    }
}